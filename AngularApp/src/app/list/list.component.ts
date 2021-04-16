import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router'
import { HttpService} from '../http.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
 //object | undefined is what the example gave
  movies: any = [];
  searches: any;

  movies2: any;
  searches2: any;
  searchTerm: any;
  pageNum: any;
  nextPg: any;
  prevPg: any;

  searchForm!: FormGroup;

  constructor(private router :ActivatedRoute, private _http: HttpService) { }

  ngOnInit(): void {

    this.searchForm = new FormGroup({
      search: new FormControl('', Validators.minLength(2))
    });
    
    console.log(this.router.snapshot.params);
    this.searchTerm = this.router.snapshot.params.search;
    this.pageNum = (this.router.snapshot.params.page -1 ) * 2 + 1;
    if (this.pageNum < 75)
    {  
      this.nextPg = parseInt(this.router.snapshot.params.page) + 1;
    }
    if (this.pageNum > 1)
    {
      this.prevPg = this.router.snapshot.params.page - 1;
    }
    this._http.getMovies(this.searchTerm,this.pageNum).subscribe(data => {
      this.movies = data;
      this.searches = this.movies.Search;
      console.log("this is movies now just so you know");
      console.log(this.movies.Search);
      
    });
    this._http.getMovies(this.searchTerm,(this.pageNum + 1)).subscribe(data => {
      this.movies2 =  data;
      
      this.searches2 = this.movies2.Search;
      console.log("this is movies now just so you know");
      console.log(this.movies2);
    });

  }

  increasePage(){
    this.pageNum++;
    return this.pageNum;
  }

  decreasePage(){
    this.pageNum--;
    return this.pageNum;
  }
  getPreviousPageNum(){
    this.prevPg = this.pageNum - 1;
    console.log(this.prevPg);
    return this.prevPg;
  }

  getNextPageNum(){
    this.nextPg = this.pageNum + 1;
    console.log(this.nextPg);
    return this.nextPg;
  }

  getPageNum(){
    console.log("PageNum" + this.pageNum);
    return this.pageNum;
  }

  
  getSearchTerm(){
    console.log("Search Term" + this.searchTerm);
    return this.searchTerm;
  }
  
  onSubmit() {
    if (this.searchForm.get('search')!.value != "")
    {
      let searchParam = JSON.stringify(this.searchForm.get('search')!.value).substring(1, JSON.stringify(this.searchForm.get('search')!.value).length - 1);
      window.location.href = "/list/" +  searchParam + "/1";
    }
  }

}
