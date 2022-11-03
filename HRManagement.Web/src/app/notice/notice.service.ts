import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NoticeService {

  constructor(private _http: HttpClient) { }

  apiUrl = 'https://newsapi.org/v2/top-headlines?country=us&category=business&apiKey=55916829bab9452d9eb24ac7535c1e60';

  topHeading(): Observable<any>
  {
    return this._http.get(this.apiUrl);
  }
}
