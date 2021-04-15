import { HttpClientTestingModule  /*,HttpTestingController*/ } from '@angular/common/http/testing';
import { Type } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpService} from '../http.service'
import { RouterTestingModule } from '@angular/router/testing';
import { LoginService} from '../login.service';

import { LoginComponent } from './login.component';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {    TestBed.configureTestingModule({
    imports: [
      RouterTestingModule,
      HttpClientTestingModule
    ],
    declarations: [
      LoginComponent
    ],
    providers : [
      HttpService,
      LoginService
    ]
  }).compileComponents();
});

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it("should return username", () =>{
    component.userName = "rwplyler";
    expect(component.getUserName()).toBe("rwplyler");
  });
  it("should return true", () =>{
    component.password = "yes";
    expect(component.isPasswordRigt("yes")).toBeTrue;
  }) 

  it('Switch to Regisrer', () => {
    component.isLoginPage = true;
    component.switchToRegister();
    expect(component.isLoginPage).toBe(false);
  });

  it('Switch to Regisrer', () => {
    component.isLoginPage = false;
    component.backToLogin();
    expect(component.isLoginPage).toBe(true);
  });
});
