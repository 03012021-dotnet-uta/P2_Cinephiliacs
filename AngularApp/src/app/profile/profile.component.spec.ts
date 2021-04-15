import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule  /*,HttpTestingController*/ } from '@angular/common/http/testing';
import { Type } from '@angular/core';

import {HttpService} from '../http.service'
import { RouterTestingModule } from '@angular/router/testing';
import { LoginService} from '../login.service';
import { ProfileComponent } from './profile.component';
import { DiscussionComponent } from '../discussion/discussion.component';

describe('ProfileComponent', () => {
  let component: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;

  
    beforeEach(async () => {    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      declarations: [
        ProfileComponent
      ],
      providers : [
        HttpService,
        LoginService
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('Movies should be loaded', () =>{
    component.moviesLoaded();
    expect(component.moviesAreLoaded).toBe(true);
  })

  it('Reviews should be loaded', () =>{
    component.reviewsLoaded();
    expect(component.reviewsAreLoaded).toBe(true);
  })
  it("Discussions should be loaded", () =>{
    component.dicussionsLoaded();
    expect(component.discussionsAreLoaded).toBeTrue;
  })
  it('commentsLoaded', ()=>{
    component.commentsLoaded();
    expect(component.commentsAreLoaded).toBeTrue;
    })




});
