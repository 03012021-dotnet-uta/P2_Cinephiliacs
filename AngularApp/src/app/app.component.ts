import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators} from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'my-app';
  test: string = '';

  headerSearch!: FormGroup;

  constructor() { }

  ngOnInit (): void {
    this.headerSearch = new FormGroup({
      headSearch: new FormControl('', Validators.minLength(2))
    });
  }
  
  reloadPage(){
    console.log("reload?");
    window.location.reload();
  }

  onSubmit() {
    if (this.headerSearch.get('headSearch') != null)
    {
      let searchParam = JSON.stringify(this.headerSearch.get('headSearch')!.value).substring(1, JSON.stringify(this.headerSearch.get('headSearch')!.value).length - 1);
      window.location.href = "/list/" +  searchParam + "/1";
    }
  }
}
