import React from 'react';
import { useLocation } from 'react-router-dom';

const KonnectSlider = () => {
  const location = useLocation();
  let flag = true;
  if (location.pathname != '/Home' && location.pathname != '/') {
    flag = false;
  }
  return (
    <div
      class='konnect-carousel carousel-image carousel-image-pagination carousel-image-arrows flexslider'
      style={{ display: flag ? 'block' : 'none' }}
    >
      <ul class='slides'>
        <li class='item'>
          <div class='container'>
            <div class='row pos-rel'>
              <div class='col-sm-12 col-md-6 animate'>
                <h1 class='big fadeInDownBig animated'>
                  Online and Class Room Training
                </h1>
                <p class='normal fadeInUpBig animated delay-point-five-s'>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                  Mauris in tincidunt mauris. Etiam arcu enim, laoreet vitae
                  orci vel, rutrum feugiat nibh. Integer feugiat ligula tellus,
                  non pulvinar justo pharetra eu. Nullam vehicula lorem ut diam
                  tincidunt sagittis. Morbi est ligula, posuere in laoreet ac,
                  porta porttitor dui
                </p>
                <a
                  class='btn btn-bordered btn-white btn-lg fadeInRightBig animated delay-one-s'
                  href='#'
                >
                  {' '}
                  Show more{' '}
                </a>{' '}
              </div>
              <div class='col-md-6 animate pos-sta hidden-xs hidden-sm'>
                {' '}
                <img
                  class='img-responsive img-right fadeInUpBig animated delay-one-point-five-s'
                  alt='iPhone'
                  src='img/slider/student-1.png'
                />{' '}
              </div>
            </div>
          </div>
        </li>

        <li class='item'>
          <div class='container'>
            <div class='row pos-rel'>
              <div class='col-md-6 animate pos-sta hidden-xs hidden-sm'>
                {' '}
                <img
                  class='img-responsive img-left fadeInUpBig animated'
                  alt='Circle'
                  src='img/slider/student-2.png'
                />{' '}
              </div>
              <div class='col-sm-12 col-md-6 animate'>
                <h2 class='big fadeInUpBig animated delay-point-five-s'>
                  Based on Bootstrap
                </h2>
                <p class='normal fadeInDownBig animated delay-one-s'>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                  Mauris in tincidunt mauris. Etiam arcu enim, laoreet vitae
                  orci vel, rutrum feugiat nibh. Integer feugiat ligula tellus,
                  non pulvinar justo pharetra eu. Nullam vehicula lorem ut diam
                  tincidunt sagittis. Morbi est ligula, posuere in laoreet ac,
                  porta porttitor dui
                </p>
                <a
                  class='btn btn-bordered btn-white btn-lg fadeInLeftBig animated delay-one-point-five-s'
                  href='#'
                >
                  {' '}
                  Show more{' '}
                </a>{' '}
              </div>
            </div>
          </div>
        </li>

        <li class='item'>
          <div class='container'>
            <div class='row pos-rel'>
              <div class='col-sm-12 col-md-6 animate'>
                <h2 class='big fadeInLeftBig animated'>Clean and Flat</h2>
                <p class='normal fadeInRightBig animated delay-point-five-s'>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                  Mauris in tincidunt mauris. Etiam arcu enim, laoreet vitae
                  orci vel, rutrum feugiat nibh. Integer feugiat ligula tellus,
                  non pulvinar justo pharetra eu. Nullam vehicula lorem ut diam
                  tincidunt sagittis. Morbi est ligula, posuere in laoreet ac,
                  porta porttitor dui
                </p>
                <a
                  class='btn btn-bordered btn-white btn-lg fadeInUpBig animated delay-one-s'
                  href='#'
                >
                  {' '}
                  Show more{' '}
                </a>{' '}
              </div>
              <div class='col-md-6 animate pos-sta hidden-xs hidden-sm'>
                {' '}
                <img
                  class='img-responsive img-right fadeInUpBig animated delay-one-point-five-s'
                  alt='Man'
                  src='img/slider/student-3.png'
                />{' '}
              </div>
            </div>
          </div>
        </li>
      </ul>
    </div>
  );
};

export default KonnectSlider;
