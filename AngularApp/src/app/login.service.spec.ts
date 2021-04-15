import { HttpClientTestingModule ,HttpTestingController} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { LoginService } from './login.service';

describe('LoginService', () => {
  let service: LoginService, 
    testingController: HttpTestingController;

beforeEach(() => {
  TestBed.configureTestingModule({
    imports: [HttpClientTestingModule],
    providers: [LoginService]
  });
  service = TestBed.inject(LoginService);
  testingController = TestBed.get(HttpTestingController);
});

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('GetURL should be right', () => {
    expect(service.getURL()).toBe("https://cinephiliacsapi.azurewebsites.net/");
  });

  it('Create User',() =>{
    expect(service.loginUser).toBeTruthy();
  });

  it('PostReview',() =>{
    expect(service.postReview).toBeTruthy();
  });

  it('Get User',() =>{
    expect(service.getUser).toBeTruthy();
  });

  it('Update User',() =>{
    expect(service.postUpdateUser).toBeTruthy();
  });
   it('get connection',() =>{
     service.connection = "www.google.com";
     expect(service.getConnection()).toBe("www.google.com");
   });

   it('get Asking User', () =>{
      service.askingUser = "rwplyler";
      expect(service.getAskingUser()).toBe("rwplyler");
   });

   it('get current User', () =>{
    service.currentUser= "rwplyler";
    expect(service.getCurrentUser()).toBe("rwplyler");
 });

  it('getDiscussion',() =>{
    expect(service.getDiscussionComments).toBeTruthy();
  });

  it('PostComment',() =>{
    expect(service.postComment).toBeTruthy();
  });

  
  it('followMovie',() =>{
    expect(service.followMovie).toBeTruthy();
  });
});
