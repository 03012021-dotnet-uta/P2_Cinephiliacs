
import { HomeComponent } from './home.component';
import { HttpClientTestingModule  /*,HttpTestingController*/ } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpService} from '../http.service'
import { RouterTestingModule } from '@angular/router/testing';
import { LoginService} from '../login.service';
describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;


  
  beforeEach(async () => {    
    TestBed.configureTestingModule({
    imports: [
      RouterTestingModule,
      HttpClientTestingModule
    ],
    declarations: [
      HomeComponent
    ],
    providers : [
      HttpService,
      LoginService
    ]
  }).compileComponents();
});
  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should logout true', () => {
    expect(component.logout).toBeTruthy();
  });

  it('should logout correctly', () => {
    component.logout();
    expect(component.currentUser.username).toBe("");
  });
  
  it('should return user name ', () => {
    component.currentUser = {
      username:'rwplyler',
      firstname:'',
      lastname:'',
      email:'',
      permissions:1
    }
    expect(component.getCurrentUser()).toBe('rwplyler');
  });

});
