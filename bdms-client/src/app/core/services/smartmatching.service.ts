import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class SmartMatchingService {
  constructor(private http: HttpClient) { }
  smartMatch(requestId: number) {
    return this.http.get<any[]>(`${environment.apiUrl}/SmartMatching/smart-match/${requestId}`);
  }

  notifyDonors(data: any) {
    return this.http.post(`${environment.apiUrl}/SmartMatching/notify-donor`, data);
  }
}
