import React from 'react';
import './styles/App.css';
import AppRoute from './navigations/AppRoute';
import {BrowserRouter} from 'react-router-dom';
import {observer} from 'mobx-react-lite';

function App() {

    return (
      <BrowserRouter>
        <div className='App'>
          <div className="wrapper">
            <AppRoute/>
          </div>
        </div>
      </BrowserRouter>
  );
}

export default App;