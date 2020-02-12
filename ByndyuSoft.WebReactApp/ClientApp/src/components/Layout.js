import React from 'react';
import './layout.css';


export function Layout(props) {
  return (
    <div className="calc-layout d-flex h-100 p-3 mx-auto flex-column">
      <header className="mb-auto">
        <div className="inner">
          <h3>Тестовый калькулятор</h3>
        </div>
      </header>

      <main role="main" className="inner">
        { props.children }
      </main>

      <footer className="mt-auto">
        <div className="inner">
          <p>© 2020 | Тестовое задание для Byndyusoft | <b>Мурзин А.В.</b> <a href="mailto:andreu_mail@mail.ru">andreu_mail@mail.ru</a></p>
        </div>
      </footer>
    </div>
  );
}
