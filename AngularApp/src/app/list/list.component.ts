import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { HttpService} from '../http.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
 //object | undefined is what the example gave
  movies: any;
  searches: any;
  searchTerm: any;
  pageNum: any;

  constructor(private router :ActivatedRoute, private _http: HttpService) { }

  ngOnInit(): void {
    console.log(this.router.snapshot.params);
    this.searchTerm = this.router.snapshot.params.search;
    this.pageNum = this.router.snapshot.params.page;
    this._http.getMovies(this.searchTerm,this.pageNum).subscribe(data => {
      this.movies = data;
      this.searches = this.movies.Search;
      console.log("this is movies now just so you know");
      console.log(this.movies.Search);
    });
  }

}
