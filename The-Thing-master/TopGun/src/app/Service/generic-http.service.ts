import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenericHttpService {

  constructor(private httpClient: HttpClient, ) { }

  public post<T>(url: string, item?: T): Observable<T> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: ''
    });

    const options = { headers };

    if (item === undefined) {
      return this.httpClient.post<T>(url, options);
    } else {
      return this.httpClient.post<T>(url, item, options);
    }


  }


  get<T>(url: string, item: T): Observable<T> {

    return this.httpClient.get<T>(url, item);
  }

  delete<T>(url: string, item: T) {
    return this.httpClient.delete(url, item);
  }

}
