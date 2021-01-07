import React from 'react';


import { Link } from 'react-router-dom';
import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';
const Faq=()=>
{
    return(
        <>
        <TopBar/>
        <Nav/>
       
      
      <section>
        <div class="container">
        <header class="inner"> 

  <div class="header-content">
  <div class="container">
    <div class="row">
      <div class="col-sm-12">
        <h1 id="homeHeading"><Link to="/">Home</Link> / <span>Faq</span></h1>
      </div>
    </div>
  </div>
  </div>
</header>
          <div class="row"> 
            <div class="col-md-12">
              <h2 class="para-heading ">Frequently Asked Questions</h2>
              <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h4 class="panel-title"> <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" aria-expanded="true">Question 1</a> </h4>
                  </div>
                  <div id="collapse1" class="panel-collapse collapse in">
                    <div class="panel-body">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur lobortis, lorem velefficitur interdum, massa lacus hendrerit mi.Duis feugiat metus quis nunc rutrum, sit amet sagittis eros varius. Proin nulla dolor, porta id vestibulum nec, tempus id purus. In viverra commodo tellus, vitae iaculis dui rutrum eu. Cum sociis natoque penatibus et magnis dis parturient montes.</div>
                  </div>
                </div>
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h4 class="panel-title"> <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Question 2</a> </h4>
                  </div>
                  <div id="collapse2" class="panel-collapse collapse">
                    <div class="panel-body">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur lobortis, lorem velefficitur interdum, massa lacus hendrerit mi.Duis feugiat metus quis nunc rutrum, sit amet sagittis eros varius. Proin nulla dolor, porta id vestibulum nec, tempus id purus. In viverra commodo tellus, vitae iaculis dui rutrum eu. Cum sociis natoque penatibus et magnis dis parturient montes.</div>
                  </div>
                </div>
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h4 class="panel-title"> <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Question 3</a> </h4>
                  </div>
                  <div id="collapse3" class="panel-collapse collapse">
                    <div class="panel-body">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur lobortis, lorem velefficitur interdum, massa lacus hendrerit mi.Duis feugiat metus quis nunc rutrum, sit amet sagittis eros varius. Proin nulla dolor, porta id vestibulum nec, tempus id purus. In viverra commodo tellus, vitae iaculis dui rutrum eu. Cum sociis natoque penatibus et magnis dis parturient montes.</div>
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
              <a href="javascript:void(0)" class="template-button"><i class="fa fa-angle-double-right" aria-hidden="true"></i> Contact Now</a> </div>
          </div>
        </div>
      </aside>
      <KonnectSlider/>
      <Footer/>
        </>
    )
}
export default Faq;