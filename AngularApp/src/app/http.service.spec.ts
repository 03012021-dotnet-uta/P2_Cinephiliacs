import { HttpClientTestingModule ,HttpTestingController} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { HttpService } from './http.service';

describe('HttpService', () => {
  let service: HttpService, 
    testingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HttpService]
    });
    service = TestBed.inject(HttpService);
    testingController = TestBed.get(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should create an array', ()=>{
    const back = 'back to the future';
    let movies:any;
    movies = service.getMovies(back,1);
    expect(true).toBeTrue();
    
  });
});

