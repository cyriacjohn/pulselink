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
    return this.http.post(`${environment.apiUrl}/donation`, { donorId, hospitalId }, { responseType: 'blob' });
  }
}
