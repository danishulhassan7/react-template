import React from 'react';
import TopCard from './TopCard.js';
import './style.css';


import TopBar from './TopBar';
import Nav from './Nav';
import KonnectSlider from './KonnectSlider';
import Footer from './Footer';

class Single extends React.Component{
    render(){
        return(
            <div>
            <TopBar/>
            <Nav/>
           <TopCard pagename= "Single Blog"/>
            <section className="section-bottom-border">
              <div className="container">
                <div className="row">
                  <div className="col-md-8 list-container post"> 
                    {/* Post Social Sharing icons */}
                    <div className="post-social-share"> <span>Share On</span> <i className="fa fa-facebook fa-2x" aria-hidden="true" /><i className="fa fa-twitter fa-2x" aria-hidden="true" /><i className="fa fa-google-plus fa-2x" aria-hidden="true" /><i className="fa fa-whatsapp fa-2x" aria-hidden="true" /><i className="fa fa-linkedin fa-2x" aria-hidden="true" /> </div>
                    {/* Post description */}
                    <hr />
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus a consequat mi. Mauris ullamcorper lacinia elit, at porttitor elit facilisis nec. Sed eleifend ornare turpis ac scelerisque. Donec in euismod erat. Cras luctus dapibus nibh, ac tincidunt magna semper sit amet. Nullam ligula lectus, convallis nec turpis id, efficitur convallis diam. Nunc ut leo tincidunt, interdum neque vitae, pulvinar nunc. Mauris quis placerat dui, non commodo tortor. Duis quis dolor massa. In placerat molestie facilisis. Sed id tempus nulla. In blandit porta mi, vel accumsan eros consequat eget.</p>
                    <a href="javascript:void(0)"> <img className="img-responsive" src="img/news/news1.jpg" alt="image" /> </a> <span className="caption text-muted">Donec fringilla nunc enim, sed posuere velit pellentesque ut.</span>
                    <p>Sed a pulvinar risus. Donec aliquam tincidunt nunc, eget eleifend eros maximus a. Praesent quis quam eros. Mauris tristique leo a lorem auctor, vel rutrum eros tempor. Donec fringilla nunc enim, sed posuere velit pellentesque ut. Integer faucibus massa vitae magna ullamcorper, vulputate sagittis metus dictum. Donec lobortis, lorem in vehicula eleifend, velit lectus viverra libero, et tincidunt augue ante quis velit. Donec in volutpat diam.</p>
                    <h2 className="single-post-heading">Heaging comes here</h2>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus a consequat mi. Mauris ullamcorper lacinia elit, at porttitor elit facilisis nec. Sed eleifend ornare turpis ac scelerisque. Donec in euismod erat. Cras luctus dapibus nibh, ac tincidunt magna semper sit amet. Nullam ligula lectus, convallis nec turpis id, efficitur convallis diam. Nunc ut leo tincidunt, interdum neque vitae, pulvinar nunc. Mauris quis placerat dui, non commodo tortor. Duis quis dolor massa. In placerat molestie facilisis. Sed id tempus nulla. In blandit porta mi, vel accumsan eros consequat eget.</p>
                    {/* post blockquote */}
                    <blockquote>Sed eleifend ornare turpis ac scelerisque. Donec in euismod erat. Cras luctus dapibus nibh, ac tincidunt magna semper sit amet.</blockquote>
                    <p>Nullam ligula lectus, convallis nec turpis id, efficitur convallis diam. Nunc ut leo tincidunt, interdum neque vitae, pulvinar nunc. Mauris quis placerat dui, non commodo tortor. Duis quis dolor massa. In placerat molestie facilisis. Sed id tempus nulla. In blandit porta mi, vel accumsan eros consequat eget.</p>
                    {/* Post Sub heading */}
                    <h2 className="single-post-heading">Heading comes here</h2>
                    <p>Sed a pulvinar risus. Donec aliquam tincidunt nunc, eget eleifend eros maximus a. Praesent quis quam eros. Mauris tristique leo a lorem auctor, vel rutrum eros tempor. Donec fringilla nunc enim, sed posuere velit pellentesque ut. Integer faucibus massa vitae magna ullamcorper, vulputate sagittis metus dictum. Donec lobortis, lorem in vehicula eleifend, velit lectus viverra libero, et tincidunt augue ante quis velit. Donec in volutpat diam.</p>
                    <p>Quisque nec est sit amet dui vulputate cursus. Vestibulum gravida mollis elit. Donec lectus tellus, suscipit at vehicula vel, cursus at lorem. Vivamus ac nibh in nisl vulputate congue. Nam non ex a quam lobortis vehicula sit amet sit amet lorem. Donec nec sagittis urna, sit amet luctus tortor. Duis leo ipsum, rhoncus a dui eu, interdum dapibus quam.</p>
                    <p>Nullam ligula lectus, convallis nec turpis id, efficitur convallis diam. Nunc ut leo tincidunt, interdum neque vitae, pulvinar nunc. Mauris quis placerat dui, non commodo tortor. Duis quis dolor massa. In placerat molestie facilisis. Sed id tempus nulla. In blandit porta mi, vel accumsan eros consequat eget.</p>
                    {/* post tags */}
                    <h3>Tags</h3>
                    <div className="sidebar-tags"> <a href="javascript:void(0)">HTML5</a> <a href="javascript:void(0)">Bootstrap</a> <a href="javascript:void(0)">CSS3</a> <a href="javascript:void(0)">jquery</a> </div>
                    <hr />
                    {/* post social sharing icons */}
                    <div className="post-social-share"> <span>Share On</span> <i className="fa fa-facebook fa-2x" aria-hidden="true" /><i className="fa fa-twitter fa-2x" aria-hidden="true" /><i className="fa fa-google-plus fa-2x" aria-hidden="true" /><i className="fa fa-whatsapp fa-2x" aria-hidden="true" /><i className="fa fa-linkedin fa-2x" aria-hidden="true" /> </div>
                    <hr />
                    {/* Pager */}
                    <ul className="pager">
                      <li className="prev"> <a href="javascript:void(0)">← Prev</a> </li>
                      <li className="next"> <a href="javascript:void(0)">Next →</a> </li>
                    </ul>
                    {/* comments */}
                    <hr />
                    <h3 className = "myheading"> Comments</h3>
                    <div className="post-footer">
                      <div className="input-group">
                        <input className="form-control comment-btn" placeholder="Add a comment" type="text" />
                        <span className="input-group-addon"> <i className="fa fa-edit" /> </span> </div>
                      <ul className="comments-list">
                        <li className="comment"> <a className="pull-left" href="javascript:void(0)"> <img className="avatar" src="img/user.png" alt="avatar" /> </a>
                          <div className="comment-body">
                            <div className="comment-heading">
                              <h4 className="user">Gestino</h4>
                              <h5 className="time">5 minutes ago</h5>
                            </div>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc et semper orci, vel vulputate tellus.</p>
                          </div>
                          <ul className="comments-list">
                            <li className="comment"> <a className="pull-left" href="javascript:void(0)"> <img className="avatar" src="img/user2.png" alt="avatar" /> </a>
                              <div className="comment-body">
                                <div className="comment-heading">
                                  <h4 className="user">David</h4>
                                  <h5 className="time">3 minutes ago</h5>
                                </div>
                                <p>ok, we will make it</p>
                              </div>
                            </li>
                            <li className="comment"> <a className="pull-left" href="javascript:void(0)"> <img className="avatar" src="img/user3.png" alt="avatar" /> </a>
                              <div className="comment-body">
                                <div className="comment-heading">
                                  <h4 className="user">Thomos</h4>
                                  <h5 className="time">3 minutes ago</h5>
                                </div>
                                <p>Ok, cool.</p>
                              </div>
                            </li>
                          </ul>
                        </li>
                      </ul>
                    </div>
                    {/* comments end */} 
                  </div>
                  {/* ==== Sidebar Starts Here ==== */}
                  <div className="col-md-4 sidebar"> 
                    {/*Sidebar Social Links*/}
                    <div className="sidebar-social"> <a href="javascript:void(0)"><i className="fa fa-facebook" aria-hidden="true" /></a> <a href="javascript:void(0)"><i className="fa fa-twitter" aria-hidden="true" /></a> <a href="javascript:void(0)"><i className="fa fa-google-plus" aria-hidden="true" /></a> <a href="javascript:void(0)"><i className="fa fa-whatsapp" aria-hidden="true" /></a> <a href="javascript:void(0)"><i className="fa fa-linkedin" aria-hidden="true" /></a> <a href="javascript:void(0)"><i className="fa fa-youtube" aria-hidden="true" /></a> </div>
                    <hr />
                    {/*Sidebar Popular Posts*/}
                    <h2>Popular Posts</h2>
                    <div className="sidebar-post"> <a href="javascript:void(0)">Curabitur efficitur malesuada velit, in ultricies nisi ornare eu.</a>
                      <p className="post-meta">Posted by <a href="javascript:void(0)">Author Name</a> on July 8, 2016</p>
                    </div>
                    <div className="sidebar-post"> <a href="javascript:void(0)">Curabitur efficitur malesuada velit, in ultricies nisi ornare eu.</a>
                      <p className="post-meta">Posted by <a href="javascript:void(0)">Author Name</a> on July 8, 2016</p>
                    </div>
                    <div className="sidebar-post"> <a href="javascript:void(0)">Curabitur efficitur malesuada velit, in ultricies nisi ornare eu.</a>
                      <p className="post-meta">Posted by <a href="javascript:void(0)">Author Name</a> on July 8, 2016</p>
                    </div>
                    <div className="sidebar-post sidebar-post-last"> <a href="javascript:void(0)">Curabitur efficitur malesuada velit, in ultricies nisi ornare eu.</a>
                      <p className="post-meta">Posted by <a href="javascript:void(0)">Author Name</a> on July 8, 2016</p>
                    </div>
                    <hr />
                    {/*Sidebar Call to Action*/}
                    <div className="sidebar-cta">
                      <div> <img src="img/e-book.png" alt="e book" />
                        <h3>Please Download E-book</h3>
                        <button className="btn-download">Download</button>
                      </div>
                    </div>
                    <hr />
                    {/*Sidebar Categories*/}
                    <h2>Categories</h2>
                    <ul className="sidebar-list">
                      <li><a href="javascript:void(0)">Category one</a></li>
                      <li><a href="javascript:void(0)">Category two</a></li>
                      <li><a href="javascript:void(0)">Category three</a></li>
                      <li><a href="javascript:void(0)">Category four</a></li>
                      <li><a href="javascript:void(0)">Category five</a></li>
                      <li><a href="javascript:void(0)">Category six</a></li>
                    </ul>
                    <hr />
                    {/*Sidebar Popular Tags*/}
                    <h2>Popular Tags</h2>
                    <div className="sidebar-tags"> <a href="javascript:void(0)">HTML5</a> <a href="javascript:void(0)">Bootstrap</a> <a href="javascript:void(0)">CSS3</a> <a href="javascript:void(0)">jquery</a> <a href="javascript:void(0)">Blog</a> <a href="javascript:void(0)">HTML Blog</a> <a href="javascript:void(0)">Themeforest</a> <a href="javascript:void(0)">Sidebar</a> </div>
                  </div>
                  {/* ==== Sidebar Ends Here ==== */} 
                </div>
              </div>
            </section>
            <KonnectSlider/>
            <Footer/>
          </div>
        );
    }
}
export default Single;