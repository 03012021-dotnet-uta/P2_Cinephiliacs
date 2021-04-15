  
import { HttpClientTestingModule  /*,HttpTestingController*/ } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpService} from '../http.service'
import { ListComponent } from './list.component';
import * as Rx from 'rxjs';
import { delay } from "rxjs/operators";
import { RouterTestingModule } from '@angular/router/testing';

describe('ListComponent', () => {
  let component: ListComponent;
  let fixture: ComponentFixture<ListComponent>;

  beforeEach(async () => {    TestBed.configureTestingModule({
    imports: [
      RouterTestingModule,
      HttpClientTestingModule
    ],
    declarations: [
      ListComponent
    ],
    providers : [
      HttpService
    ]
  }).compileComponents();
});

  beforeEach(() => {
    fixture = TestBed.createComponent(ListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should increment "myNumber"', () => {
    component.function1();
    expect(component.myNumber).toBe(1);
    component.function1();
    expect(component.myNumber).toBe(2);
  });

  it('should concatenate the strings', () => {
    component.function2('testing', ' is fun');
    expect(component.myStrings).toBe('testing is fun');
  });

  it('should concatenate and return the string', () => {
    expect(component.function3('test')).toBe('testtest', 'funtion3 test failed.');
  });


});
