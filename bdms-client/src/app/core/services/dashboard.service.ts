import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DashboardService {
  constructor(private http: HttpClient) { }
  get(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/dashboard`);
  }
}
