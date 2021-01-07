import React, { useState } from 'react';
import {withRouter} from 'react-router'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import './App.css';
import Single from './components/Single.js';
import "./css/animate.css";
import "./css/theme.css";

import './assets/css/style.css';
import "./assets/bootstrap/css/bootstrap.css";
import "./css/konnect-slider.css";
import "./css/green.css";

import Gallery from './components/Gallery';
import Home from './components/Home';
import Login from './components/Login';
import Register from './components/Register';
import About from './components/About';
import Card from './components/Card'
import ApplyCourse from './components/ApplyCourse';
import Course from './components/Course';
import SingleCourse from './components/SingleCourse';
import Contact from './components/Contact';
import Error from './components/Error';

import Articles from './components/Articles';
import Faq from './components/Faq';
import Privacy from './components/Privacy';
import TermsCondition from './components/TermsCondition';
function App() {
  return (
    <Router>

  
      <div className='App'>
        <div className='loading'>
          <div className='loader'></div>
        </div>
        <a id='scroll-up'>
          <i className='fa fa-angle-up'></i>
        </a>

        <div className='theme-settings' id='switcher'>
          {' '}
          <span className='theme-click'>
            <i className='fa fa-cog fa-spin' aria-hidden='true'></i>
          </span>{' '}
          <span
            className='theme-color theme-default theme-active'
            data-color='green'
          ></span>{' '}
          <span className='theme-color theme-blue' data-color='blue'></span>{' '}
          <span className='theme-color theme-red' data-color='red'></span>{' '}
          <span className='theme-color theme-violet' data-color='violet'></span>
          <p>(Or) Your favorite color</p>
        </div>
        
        <Switch>
          <Route exact path='/' component={Home} />
          <Route  path='/Home/register' component={Register} />
          <Route  path='/Home/About' component={About} />
          <Route  path='/Home/login' component={Login} />
          <Route  path='/Home/Articles' component={Articles} />
          <Route  path='/Home/Privacy' component={Privacy} />
          <Route  path='/Home/Faq' component={Faq} />
          <Route  path='/Home/TermsCondition' component={TermsCondition} />
          <Route  path='/Home/Blog' component={Card} />
          <Route  path='/ApplyCourse' component={ApplyCourse} />
          <Route  path='/Home/SingleCourse' component={SingleCourse} />
          <Route  path='/Home/Course' component={Course} />
          
          <Route  path='/Home/Gallery' component={Gallery} />
          <Route  path='/Home/Contact' component={Contact} />
          
          <Route component={Error} />
        
        
       
       </Switch>
        </div>
        
     
      </Router>
  );
}

export default App;
