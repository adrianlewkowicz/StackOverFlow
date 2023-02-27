import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public tags: Tags[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Tags[]>(baseUrl + 'tag').subscribe(result => {
      this.tags = result;
    }, error => console.error(error));
  }
}

