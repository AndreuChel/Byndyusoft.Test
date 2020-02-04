using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Detectors;
using Calculator.Models;

namespace Calculator.Resolvers
{
    public class ExpressionResolver : IExpressionResolver
    {
        private List<IElementDetector> _detectorList = new List<IElementDetector>();

        public IExpressionResolver AddDetector(IElementDetector detector)
        {
            _detectorList.Add(detector);
            return this;
        }

        public virtual IEnumerable<IExpressionElement> Parse(string inputExpression)
        {
            var _foundElements = new List<IExpressionElement>();

            var offset = 0;
            while (offset < inputExpression.Length)
            {
                var searchResult =
                    Enumerable.Range(1, inputExpression.Length - offset).OrderByDescending(o => o)
                              .Select(tmpLength => {
                                  var targerSearchString = inputExpression.Substring(offset, tmpLength);
                                  return new {
                                      SubString = targerSearchString,
                                      Elements = _detectorList.Select(detector => detector.GetElement(targerSearchString))
                                                              .Where(el => el != null).ToList()
                                  };
                              })
                              .FirstOrDefault(res => res.Elements.Count() > 0);

                if (searchResult == null)
                    throw new InvalidExpressionException($"Неизвестный формат строки \"{inputExpression.Substring(offset, inputExpression.Length - offset)}\"");


                if (searchResult.Elements.Count() > 1)
                {
                    throw new InvalidResolverConfigureException($"Строку \"{searchResult.SubString}\" распознали более одного детектора элементов выражения");
                }

                _foundElements.Add(searchResult.Elements.First());
                offset += searchResult.SubString.Length;
            }


            return _foundElements;
        }

        public IEnumerator<IElementDetector> GetEnumerator() => _detectorList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _detectorList.GetEnumerator();

    }
}
