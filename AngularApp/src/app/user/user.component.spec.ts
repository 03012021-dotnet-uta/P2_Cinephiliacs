

import { UserComponent } from './user.component';

import { HttpClientTestingModule  /*,HttpTestingController*/ } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpService} from '../http.service'
import { RouterTestingModule } from '@angular/router/testing';
import { LoginService} from '../login.service';

describe('UserComponent', () => {
  let component: UserComponent;
  let fixture: ComponentFixture<UserComponent>;

  beforeEach(async () => {    TestBed.configureTestingModule({
    imports: [
      RouterTestingModule,
      HttpClientTestingModule
    ],
    declarations: [
      UserComponent
    ],
    providers : [
      HttpService,
      LoginService
    ]
  }).compileComponents();
});
  beforeEach(() => {
    fixture = TestBed.createComponent(UserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('spoilerShown is True now', () =>{
    component.displaySpoilers = false;
    component.showSpoilers();
    expect(component.displaySpoilers).toBe(true);
  });

  it('spoilerShown is Truthy', () =>{

   expect( component.showSpoilers).toBeTruthy;

  });

  it('should get username', () =>{
    component.userName = "rwplyler";
    expect(component.getUsername()).toBe("rwplyler");
  });
  it('spoilerShown is right amount', () =>{
    component.displaySpoilers = true;
    expect(component.spoilersShown()).toBe(true);
  });

  it('should show Discussion', () => {
    component.discussionsLoaded();
    expect(component.discussionsAreLoaded).toBe(true);
  });

  it('shoul load movies', () =>{
    component.moviesLoaded();
    expect(component.moviesAreLoaded).toBe(true);
  })
  it('Should Load Discussions', () =>{
    component.reviewsLoaded();
    expect(component.reviewsAreLoaded).toBe(true);
  })
  it('should load comments', ()=>{
    component.commentsLoaded();
    expect(component.commentsAreLoaded).toBe(true);
  });


});
