import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { DonationService } from '../../../core/services/donation.service';

@Component({
  selector: 'app-my-donations',
  imports: [CommonModule],
  templateUrl: './my-donations.html',
  styleUrl: './my-donations.css',
})
export class MyDonations {
  donations: any[] = [];
  constructor(private http: HttpClient, private donationService: DonationService) { }

  ngOnInit() {
    this.http.get(`${environment.apiUrl}/donation/my`).subscribe((res: any) => {
      this.donations = res;
    });
  }

  download(id: number) {
    this.donationService.downloadCertificate(id).subscribe((res: Blob) => {
      const blob = new Blob([res], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'BloodDonationCertificate.pdf';
      a.click();

      window.URL.revokeObjectURL(url);
    })
  }

  getStatusText(status: number): string {
    switch (status) {
      case 0: return 'Pending';
        break;
      case 1: return 'Approved';
        break;
      case 2: return 'Completed';
        break;
      case 3: return 'Rejected';
        break;
      default: return '';
    }
  }
}
