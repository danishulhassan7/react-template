import React, { useState } from 'react';
import {Link} from 'react-router-dom';
import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';

const About = () => {
  const [isHover, setIsHover] = useState(true);

  const changeHeadingColors = (e) => {
    setIsHover(!isHover);
  };

  return (
 <>
 <TopBar />
 <Nav />
 <body>
 <header class="inner">
     <div class="header-content">
         <div class="container">
             <div class="row">
                 <div class="col-sm-12">
                     <h1 id="homeHeading"><Link to="/">Home</Link> / <span>Gallery</span></h1>
                 </div>
             </div>
         </div>
     </div>
 </header>

 <section>
     <div class="container">
         <div class="row">
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-1"> <img src="/Content/img/gallery/gallery-one.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-2"> <img src="/Content/img/gallery/gallery-two.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-3"> <img src="/Content/img/gallery/gallery-three.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-4"> <img src="/Content/img/gallery/gallery-four.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-3"> <img src="/Content/img/gallery/gallery-three.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-4"> <img src="/Content/img/gallery/gallery-four.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-2"> <img src="/Content/img/gallery/gallery-two.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-1"> <img src="/Content/img/gallery/gallery-one.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-1"> <img src="/Content/img/gallery/gallery-one.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-2"> <img src="/Content/img/gallery/gallery-two.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-3"> <img src="/Content/img/gallery/gallery-three.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             
             <div class="col-sm-3 gallery-box"> <a href="#" data-toggle="modal" data-target=".pop-up-4"> <img src="/Content/img/gallery/gallery-four.jpg" class="img-responsive center-block" alt=""/> </a> </div>
             <div class="col-md-12 text-center margin-10"> </div>

             <div class="modal fade pop-up-4" tabindex="-1" role="dialog" aria-hidden="true">
                 <div class="modal-dialog">
                     <div class="modal-content">
                         <div class="modal-body">
                             <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                             <img src="/Content/img/gallery/gallery-four.jpg" class="img-responsive center-block" alt=""/>
                         </div>
                     </div>
                 </div>
             </div>

         </div>
     </div>
 </section>
 <aside class="dark-bg aside-cta">
     <div class="container text-center">
         <div class="row">
             <div class="col-sm-12 col-xs-12">
                 <h3 class="margin-10 text-white cta-heading">Are you Looking for good Trainer?</h3>
                 <Link to='/Home/Contact' href="javascript:void(0)" class="template-button"><i class="fa fa-angle-double-right" aria-hidden="true"></i> Contact Now</Link>
             </div>
         </div>
     </div>
 </aside>

</body>

<KonnectSlider/>
<Footer/>
 </>
  );
};

export default About;
