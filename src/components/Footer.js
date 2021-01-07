import React from 'react';

import { Link } from 'react-router-dom';
const Footer = () => {
  return (
    <div>
      {/*<main Footer>*/}
      <section className='main-footer'>
        <div className='container'>
          <div className='row'>
            {/* footer widget one */}
            <div className='col-md-4 col-sm-6'>
              <div className='footer-widget'>
                {' '}
                <img
                  src='img/logo-green.png'
                  alt=''
                  className='img-responsive logo-change'
                />
                <p>
                  Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                  Culpa consectetur assumenda est unde animi. Repellat quibusdam
                  et ad aut molestias, facere maxime expedita aperiam sint.
                </p>
                <span>
                  <a href='#' className='read-more'>
                    Read more
                  </a>
                </span>{' '}
              </div>
            </div>
            {/* /footer widget one */}

            {/* footer widget two */}
            <div className='col-md-4 col-sm-6'>
              <div className='footer-widget address'>
                <h3>Contact</h3>
                <p>
                  <i className='fa fa-address-card-o' aria-hidden='true'></i>{' '}
                  <span>
                    #Koramangala, Banglore <br /> Karnataka, INDIA
                  </span>
                </p>
                <p>
                  <i className='fa fa-envelope-o' aria-hidden='true'></i>{' '}
                  <span>youremail@yourdomain.com</span>
                </p>
                <p>
                  <i
                    className='fa fa-volume-control-phone'
                    aria-hidden='true'
                  ></i>{' '}
                  <span>
                    +88 (0) 101 0000 000 <br />
                    +88 (0) 202 0000 001
                  </span>
                </p>
              </div>
            </div>
            {/* footer widget two */}

            {/* footer widget three */}
            <div class='col-md-4 col-sm-6'>
              <div class='footer-widget quicl-links'>
                <h3>Quick Links</h3>
                <ul>
                  <li>
                    <i class='fa  fa-angle-right'></i>{' '}
                    <a href='#'>Online Classes</a>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i>{' '}
                    <a href='#'>Class Room Classes</a>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i> <a href='#'>Events</a>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i> <Link to='/Home/About'>About Us</Link>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i>{' '}
                    <Link to='/Home/Contact'>Contact Us</Link>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i> <Link to='/Home/Faq'>Faq</Link>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i> <Link to='/Home/login'>Login</Link>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i> <Link to='/Home/Register'>Sign Up</Link>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i>{' '}
                    <Link to='/Home/TermsCondition'>Terms And Conditions</Link>
                  </li>
                  <li>
                    <i class='fa  fa-angle-right'></i>{' '}
                    <Link to='/Home/Privacy'>Privacy Policy</Link>
                  </li>
                </ul>
              </div>
            </div>
            {/* footer widget three */}
          </div>
        </div>
      </section>
      {/*</Main footer>*/}

      {/*</copyright footer>*/}
      <footer>
        <div class='container'>
          <div class='row'>
            <div class='col-md-6 col-sm-6 text-left'>
              {/* Footer Social Icons */}
              <div class='contact-social'>
                <p>
                  <a href='#'>
                    {' '}
                    <i class='fa fa-twitter'></i>{' '}
                  </a>{' '}
                  <a href='#'>
                    {' '}
                    <i class='fa fa-facebook'></i>{' '}
                  </a>{' '}
                  <a href='#'>
                    {' '}
                    <i class='fa fa-google-plus'></i>{' '}
                  </a>{' '}
                  <a href='#'>
                    {' '}
                    <i class='fa fa-rss'></i>{' '}
                  </a>{' '}
                  <a href='#'>
                    {' '}
                    <i class='fa fa-instagram'></i>{' '}
                  </a>
                </p>
              </div>
            </div>

            {/*Footer Copy rights-->*/}
            <div class='col-md-6 col-sm-6 text-right'>
              <p> &copy; Copyright 2017 Konnect plugins </p>
            </div>
          </div>
        </div>
      </footer>
    </div>
  );
};

export default Footer;
