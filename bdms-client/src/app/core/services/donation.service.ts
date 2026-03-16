import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DonationService {

  constructor(private http: HttpClient) { }

  donate(donorId: number, hospitalId: number) {
    return this.http.post(`${environment.apiUrl}/donation`, { donorId: donorId, hospitalId: hospitalId }, { responseType: 'blob' });
  }

  approve(donorId: number) {
    return this.http.post(`${environment.apiUrl}/donation/${donorId}/approve`, {});
  }

  reject(donorId: number) {
    return this.http.post(`${environment.apiUrl}/donation/${donorId}/reject`, {});
  }

  getAll(status?: number | null) {
    let url = `${environment.apiUrl}/donation`;
    if (status !== null) {
      url += `?status=${status}`;
    }
    return this.http.get<any[]>(url);
  }

  donateUser(hospitalId: number) {
    return this.http.post(`${environment.apiUrl}/donation`, { hospitalId: hospitalId }, { responseType: 'blob' });
  }

  downloadCertificate(id: number) {
    return this.http.get(`${environment.apiUrl}/donation/${id}/certificate`, { responseType: 'blob'});
  }
}

