import React from 'react';
import { Link } from 'react-router-dom';

const TopBar = () => {
  const divLiStyle = {
    marginRight: '3px',
  };
  return (
    <div className='konnect-info'>
      <div className='container-fluid'>
        <div className='row'>
          <div className='col-md-6 col-sm-8 hidden-xs'>
            <ul>
              <li>
                <i className='fa fa-paper-plane' aria-hidden='true'></i>{' '}
                support@konnectplugins.com{' '}
              </li>
              <li className='li-last'>
                {' '}
                <i
                  className='fa fa-volume-control-phone'
                  aria-hidden='true'
                ></i>{' '}
                (040) 123-4567
              </li>
            </ul>
          </div>
          <div className='col-md-6 col-sm-4'>
            <ul className='konnect-float-right'>
              <li>
                <Link to='/Home/login'>
                  Login <i className='fa fa-user-o' aria-hidden='true'></i>
                </Link>
              </li>
              <li>
                <Link to='/Home/register'>
                  {' '}
                  Register
                  <i className='fa fa-file-text-o' aria-hidden='true'></i>
                </Link>
              </li>
              <li className='li-last hidden-xs hidden-sm'>
                <a target='_blank' href='#'>
                  <i className='fa fa-twitter' aria-hidden='true'></i>
                </a>{' '}
                <a target='_blank' href='#'>
                  <i className='fa fa-google-plus' aria-hidden='true'></i>
                </a>{' '}
                <a target='_blank' href='#'>
                  <i className='fa fa-facebook' aria-hidden='true'></i>
                </a>{' '}
                <a target='_blank' href='#'>
                  <i className='fa fa-linkedin' aria-hidden='true'></i>
                </a>
                <a href='#'>
                  {' '}
                  <i className='fa fa-instagram'></i>{' '}
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TopBar;
