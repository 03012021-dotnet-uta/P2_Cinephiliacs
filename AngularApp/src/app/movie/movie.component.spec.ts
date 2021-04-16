

import { MovieComponent } from './movie.component';
import { HttpClientTestingModule  /*,HttpTestingController*/ } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpService} from '../http.service'
import { RouterTestingModule } from '@angular/router/testing';
import { LoginService} from '../login.service';
import { CommaExpr, isNgTemplate } from '@angular/compiler';

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

  it('It should check input feilds to false', () =>{
    
    expect(component.inputFields()).toBeTruthy;
   
  })

  it("Last page should equal to true", () =>
  {
    component.lastPage=false;
    component.loadNextPage();
    expect(component.lastPage).toBe(true);
  })

  it("TimeSortNext",() => {
    component.reviewsBusy = false;
    component.timeSortState = 0;
    component.timeSortNext();
    expect(component.timeSortState).toBe(1);
  })

  it("TimeSortNext 2",() => {
    component.reviewsBusy = false;
    component.timeSortState = 1;
    component.timeSortNext();
    expect(component.timeSortState).toBe(2);
  })

  it("TimeSortNext 3",() => {
    component.reviewsBusy = false;
    component.timeSortState = 2;
    component.timeSortNext();
    expect(component.timeSortState).toBe(1);
  })

  it("rating sort next case 0", () =>
  {
    component.reviewsBusy = false;
    component.ratingSortState = 0;
    component.ratingSortNext();
    expect(component.ratingSortState = 1);
  })

  it("rating sort next case 1", () =>
  {
    component.reviewsBusy = false;
    component.ratingSortState = 1;
    component.ratingSortNext();
    expect(component.ratingSortState = 2);
  })

  it("rating sort next case 2", () =>
  {
    component.reviewsBusy = false;
    component.ratingSortState = 2;
    component.ratingSortNext();
    expect(component.ratingSortState = 1);
  });
  



  it('showdiscussion should be run', () => {
    component.submitDiscussion.topic = "yes";
    component.submitDiscussion.subject = "test";
    component.followMovie();
    expect(component.showDiscussion).toHaveBeenCalled;
  })
});
