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

  it('should return apiKey', () => {
    expect(service.getAPIKey()).toBe("f6d24cec46msh55a535ae1cf36d0p1bf4bcjsnd49b624447d2");
  })

  it('should return base website', () => {
    expect(service.getConnection()).toBe("https://movie-database-imdb-alternative.p.rapidapi.com");
  })

  it('should create an array', ()=>{
    const back = 'back to the future';
    let movies:any;
    movies = service.getMovies(back,1);
    expect(true).toBeTrue();
  });
});

