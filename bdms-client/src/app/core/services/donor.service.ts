import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DonorService {
  private apiUrl = `${environment.apiUrl}/donors`;
  selectedStatus: number | null = null;

  constructor(private http: HttpClient) { }
  getAll(pageNumber: number, pageSize: number, selectedStatus: number | null = null) {
    return this.http.get(`${this.apiUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}&status=${selectedStatus}`);
  }

  create(data: any) {
    return this.http.post(`${this.apiUrl}`, data);
  } 

  delete(id: string) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  update(id: string, data: any) {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  getById(id: string) {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
}
