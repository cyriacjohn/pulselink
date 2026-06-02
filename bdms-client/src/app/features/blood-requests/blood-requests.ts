import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@microsoft/signalr';

@Component({
  selector: 'app-blood-requests',
  imports: [CommonModule, FormsModule],
  templateUrl: './blood-requests.html',
  styleUrl: './blood-requests.css',
})
export class BloodRequests  {
  bloodGroup: any[] = [];

  constructor(httpClient: HttpClient) { }
}
