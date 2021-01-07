import React from 'react';


import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const Register = () => {
  return (
    <div>
    <TopBar/>
    <Nav/>
      <section className='section-bottom-border login-signup'>
        <div className='container'>
          <div className='row'>
            <div className='login-main template-form'>
              <h4>
                Please Register, or <a href='#'>Login</a>
              </h4>
              <div className='template-space'></div>
              <div className='row'>
                <div className='col-xs-6 col-sm-6 col-md-6'>
                  {' '}
                  <a href='#' className='btn btn-success btn-facebook btn-block'>
                    Facebook
                  </a>{' '}
                </div>
                <div className='col-xs-6 col-sm-6 col-md-6'>
                  {' '}
                  <a href='#' className='btn btn-danger btn-google btn-block'>
                    Google
                  </a>{' '}
                </div>
              </div>
              <div className='login-or'>
                <hr className='hr-or' />
                <span className='span-or'>or</span>{' '}
              </div>
              <form>
                <div className='form-group '>
                  <label for='inputUsername'>Username</label>
                  <input
                    id='inputUsername'
                    type='text'
                    className='form-control'
                  />
                </div>
                <div className='form-group'>
                  <label for='inputEmail'>email</label>
                  <input id='inputEmail' type='text' className='form-control' />
                </div>
                <div className='form-group'>
                  <label for='inputPassword'>Password</label>
                  <input
                    type='password'
                    className='form-control'
                    id='inputPassword'
                  />
                </div>
                <div className='form-group'>
                  <label for='inputRePassword'>Re Enter Password</label>
                  <input
                    id='inputRePassword'
                    type='password'
                    className='form-control'
                  />
                </div>
                <div className='checkbox pull-right'>
                  <label>
                    <input type='checkbox' />
                    accept Terms
                  </label>
                </div>
                <button type='submit' classNameName='btn  btn-success'>
                  {' '}
                  Sign Up{' '}
                </button>
              </form>
            </div>
          </div>
        </div>
      </section>
      <KonnectSlider/>
      <Footer/>
    </div>
  );
};

export default Register;
