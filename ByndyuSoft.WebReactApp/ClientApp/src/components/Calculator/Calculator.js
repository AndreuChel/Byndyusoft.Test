import React, { Component,Fragment } from 'react';
import debounce from 'lodash/debounce';
import './calculator.css';


export class Calculator extends Component {

    constructor(props, context) {
        super(props, context);
        this.state = { expression: '', expressionResult: 0, expressionError: '', isEditing: true };
        this.handleKeyUp = debounce(this.handleKeyUp.bind(this), 1000);
        this.handleChange = this.handleChange.bind(this);
    }

    handleKeyUp() {
        const { expression } = this.state;

        if (!expression) {
            this.setState({ ...this.state, isEditing: true } );
            return;
        }
        
        fetch('api/expression/calculate', {
            method: 'post',
            headers: { 'Content-Type' : 'application/json; charset=utf-8' },
            body: `'`+expression+`'` })
        .then(async response => response.status === 200 ? await response.json() : Promise.reject(await response.text()))
        .then(data =>   this.setState({ ...this.state, expressionResult: data, expressionError: '', isEditing: false }))
        .catch(error => this.setState({ ...this.state, expressionError: error, expressionResult: 0, isEditing: false  } ));
    }

    handleChange(event) {
        this.setState({...this.state, expression: event.target.value, isEditing: true});
    }

    render() {
        const { expression, expressionResult, expressionError, isEditing } = this.state;
        return (
            <Fragment>
                <div >
                    <label htmlFor="exp1">Выражение</label>
                    <div className="exp-block">
                        <div className="exp-input-block">
                            <input  onKeyUp = { this.handleKeyUp } onChange={ this.handleChange } value={ expression } id="exp1" placeholder="Введите математическое выражение"
                                    type="text" className="align-middle form-control is-invalid" />
                            
                            { !isEditing  && expressionError  && (
                                <span className="exp-error">{ expressionError }</span>
                            )}
                        </div>
                        { !isEditing  && !expressionError && (
                            <div className="exp-result"> 
                                <h3> { expressionResult } </h3> 
                            </div>
                        )}
                    </div>
                </div>
                <div className="exp-op-block">
                    <label>Доступные элементы</label>
                    <div>
                        <div><div className="exp-op-col1">Скобки</div><div className="exp-op-col2">( ), {'{ }'}</div></div>
                        <div><div className="exp-op-col1">Унарные операции</div><div className="exp-op-col2">+, -</div></div>
                        <div><div className="exp-op-col1">Бинарные операции</div><div className="exp-op-col2">+, -, *, /, ^</div></div>
                        <div><div className="exp-op-col1">Функции</div><div className="exp-op-col2">lg (десятичный логарифм)</div></div>
                    </div>
                </div>
            </Fragment>
        );
    }
}