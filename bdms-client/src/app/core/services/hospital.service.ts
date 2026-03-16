import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class HospitalService {

  constructor(private http: HttpClient) { }

  getHospitals() {
    const token = localStorage.getItem('token');
    return this.http.get<any[]>(`${environment.apiUrl}/hospitals`, {
      headers: {
        Authorization: `Bearer ${token}`
}
    });
  }
}
