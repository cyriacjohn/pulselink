import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { HospitalDashboard } from '../../features/hospital-dashboard/hospital-dashboard'; 

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

  getHospitalStats() {
    return this.http.get<HospitalDashboard>(`${environment.apiUrl}/hospitals/hospital-dashboard`);
  }

  getBloodGroups() {
    return this.http.get<any[]>(`${environment.apiUrl}/hospitals/bloodgroups`);
  }

  createRequest(data: any) {
    return this.http.post(`${environment.apiUrl}/hospitals/hospital-request`, data);
  }

  getOpenRequests() {
    return this.http.get<any[]>(`${environment.apiUrl}/hospitals/open-requests`);
  }
}
