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
});
