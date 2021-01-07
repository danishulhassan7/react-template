import React from 'react';
import { Link } from 'react-router-dom';


import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const Articles = () => {
  const Background = '../img/news/news1.jpg';
  return (
    <div>
    <TopBar/>
    <Nav/>
     
     
      {/*<!-- blogs  --> */}
      <section class='section-bottom-border'>
        <div class='container'>
        <header className='inner'>
        {/*<!-- Banner --> */}
        <div className='header-content'>
          <div className='container'>
            <div className='row'>
              <div className='col-sm-12'>
                <h1 id='homeHeading'>
                  <Link
                    to='/Home/About'
                    style={{
                      color: '#5dc560',
                      fontWeight: 'bold',
                      fontSize: '30px',
                    }}
                  >
                    Articles
                  </Link>
                </h1>
              </div>
            </div>
          </div>
        </div>
        </header>
          <div class='row'>
            <div class='col-md-8 list-container'>
              {/*<!--Post -->*/}
              <div class='post-preview'>
                {' '}
                <a href='blog-single.html'>
                  <div
                    class='list-thumb'
                    style={{ backgroundImage: `url(../img/news/news1.jpg)` }}
                  >
                    <div></div>
                  </div>
                  <h2 class='post-title'>
                    {' '}
                    In consequat lacus nec suscipit ullamcorper. Cras viverra
                    rhoncus est molestie sollicitudin.
                  </h2>
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on
                  September 24, 2016
                </p>
              </div>
              <hr />
              {/*<!--Post -->*/}
              <div class='post-preview'>
                {' '}
                <a href='blog-single.html'>
                  <div
                    class='list-thumb'
                    style={{ backgroundImage: `url(../img/news/news2.jpg)` }}
                  >
                    <div></div>
                  </div>
                  <h2 class='post-title'>
                    {' '}
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                    Etiam a ipsum in.{' '}
                  </h2>
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on
                  September 18, 2016
                </p>
              </div>
              <hr />
              {/*<!--Post -->*/}
              <div class='post-preview'>
                {' '}
                <a href='blog-single.html'>
                  <div
                    class='list-thumb'
                    style={{ backgroundImage: `url(../img/news/news3.jpg)` }}
                  >
                    <div></div>
                  </div>
                  <h2 class='post-title'>
                    {' '}
                    In consequat lacus nec suscipit ullamcorper. Cras viverra
                    rhoncus est molestie sollicitudin.{' '}
                  </h2>
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on
                  August 24, 2016
                </p>
              </div>
              <hr />
              {/*<!--Post -->*/}
              <div class='post-preview'>
                {' '}
                <a href='blog-single.html'>
                  <div
                    class='list-thumb'
                    style={{ backgroundImage: `url(${Background})` }}
                  >
                    <div></div>
                  </div>
                  <h2 class='post-title'>
                    {' '}
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                    Etiam a ipsum in.{' '}
                  </h2>
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on July
                  8, 2016
                </p>
              </div>
              <hr />
              {/*<!--Post -->*/}
              <ul class='pager'>
                <li class='prev'>
                  {' '}
                  <a href='javascript:void(0)'>&larr; New Posts</a>{' '}
                </li>
                <li class='next'>
                  {' '}
                  <a href='javascript:void(0)'>Older Posts &rarr;</a>{' '}
                </li>
              </ul>
            </div>

            {/*<!-- ==== Sidebar Starts Here ==== -->*/}
            <div class='col-md-4 sidebar'>
              {/*<!--Sidebar Social Links-->*/}
              <div class='sidebar-social'>
                {' '}
                <a href='javascript:void(0)'>
                  <i class='fa fa-facebook' aria-hidden='true'></i>
                </a>{' '}
                <a href='javascript:void(0)'>
                  <i class='fa fa-twitter' aria-hidden='true'></i>
                </a>{' '}
                <a href='javascript:void(0)'>
                  <i class='fa fa-google-plus' aria-hidden='true'></i>
                </a>{' '}
                <a href='javascript:void(0)'>
                  <i class='fa fa-whatsapp' aria-hidden='true'></i>
                </a>{' '}
                <a href='javascript:void(0)'>
                  <i class='fa fa-linkedin' aria-hidden='true'></i>
                </a>{' '}
                <a href='javascript:void(0)'>
                  <i class='fa fa-youtube' aria-hidden='true'></i>
                </a>{' '}
              </div>
              <hr />
              {/*<!--Sidebar Popular Posts-->*/}
              <h2>Popular Posts</h2>
              <div class='sidebar-post'>
                {' '}
                <a href='javascript:void(0)'>
                  Curabitur efficitur malesuada velit, in ultricies nisi ornare
                  eu.
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on July
                  8, 2016
                </p>
              </div>
              <div class='sidebar-post'>
                {' '}
                <a href='javascript:void(0)'>
                  Curabitur efficitur malesuada velit, in ultricies nisi ornare
                  eu.
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on July
                  8, 2016
                </p>
              </div>
              <div class='sidebar-post'>
                {' '}
                <a href='javascript:void(0)'>
                  Curabitur efficitur malesuada velit, in ultricies nisi ornare
                  eu.
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on July
                  8, 2016
                </p>
              </div>
              <div class='sidebar-post sidebar-post-last'>
                {' '}
                <a href='javascript:void(0)'>
                  Curabitur efficitur malesuada velit, in ultricies nisi ornare
                  eu.
                </a>
                <p class='post-meta'>
                  Posted by <a href='javascript:void(0)'>Author Name</a> on July
                  8, 2016
                </p>
              </div>
              <hr />
              {/*<!--Sidebar Call to Action-->*/}
              <div class='sidebar-cta'>
                <div>
                  {' '}
                  <img src='../img/e-book.png' alt='e book' />
                  <h3>Please Download E-book</h3>
                  <button class='btn-download'>Download</button>
                </div>
              </div>
              <hr />
              {/*<!--Sidebar Categories-->*/}
              <h2>Categories</h2>
              <ul class='sidebar-list'>
                <li>
                  <a href='javascript:void(0)'>Category one</a>
                </li>
                <li>
                  <a href='javascript:void(0)'>Category two</a>
                </li>
                <li>
                  <a href='javascript:void(0)'>Category three</a>
                </li>
                <li>
                  <a href='javascript:void(0)'>Category four</a>
                </li>
                <li>
                  <a href='javascript:void(0)'>Category five</a>
                </li>
                <li>
                  <a href='javascript:void(0)'>Category six</a>
                </li>
              </ul>
              <hr />
              {/*<!--Sidebar Popular Tags-->*/}
              <h2>Popular Tags</h2>
              <div class='sidebar-tags'>
                {' '}
                <a href='javascript:void(0)'>HTML5</a>{' '}
                <a href='javascript:void(0)'>Bootstrap</a>{' '}
                <a href='javascript:void(0)'>CSS3</a>{' '}
                <a href='javascript:void(0)'>jquery</a>{' '}
                <a href='javascript:void(0)'>Blog</a>{' '}
                <a href='javascript:void(0)'>HTML Blog</a>{' '}
                <a href='javascript:void(0)'>Themeforest</a>{' '}
                <a href='javascript:void(0)'>Sidebar</a>{' '}
              </div>
            </div>
            {/*<!-- ==== Sidebar Ends Here ==== --> */}
          </div>
        </div>
      </section>
    <KonnectSlider/>
    <Footer/>
    
      </div>
  );
};

export default Articles;
