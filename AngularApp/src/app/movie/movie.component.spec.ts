

import { MovieComponent } from './movie.component';
import { HttpClientTestingModule  /*,HttpTestingController*/ } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpService} from '../http.service'
import { RouterTestingModule } from '@angular/router/testing';
import { LoginService} from '../login.service';

describe('MovieComponent', () => {
  let component: MovieComponent;
  let fixture: ComponentFixture<MovieComponent>;

  beforeEach(async () => {    TestBed.configureTestingModule({
    imports: [
      RouterTestingModule,
      HttpClientTestingModule
    ],
    declarations: [
      MovieComponent
    ],
    providers : [
      HttpService,
      LoginService
    ]
  }).compileComponents();
});

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('It should check input feilds', () =>{
    
    expect(component.inputFields()).toBeTruthy;
    expect(component.caninput).toBe(false);
  })

  it('It should check input feilds to false', () =>{
    
    expect(component.inputFields()).toBeTruthy;
   
  })

  it('showdiscussion should be run', () => {
    component.submitDiscussion.topic = "yes";
    component.submitDiscussion.subject = "test";
    component.followMovie();
    expect(component.showDiscussion).toHaveBeenCalled;
  })
});
