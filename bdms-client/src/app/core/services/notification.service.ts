import * as signalR from '@microsoft/signalr';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private hubConnection!: signalR.HubConnection;

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(`${environment.apiUrl}/notifications`, { withCredentials: true }).withAutomaticReconnect().build();
    this.hubConnection.start().catch(err => console.error('Error while starting connection: ' + err));
  }

  onDonationUpdate(callback: any) {
    this.hubConnection.on("DonationUpdated", callback);
  }
}
