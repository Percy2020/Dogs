import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CalculationInput } from '../model/calculation-input';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalculationService {

  constructor(private httpClient: HttpClient) { }

  public getAktieBolag(calculationInput: CalculationInput): Observable<string> {

    const url = environment.aktiebolag_url;

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: ''
    });

    const options = { headers };

    return this.httpClient.post<any>(url, calculationInput, options);

  }
  
}
