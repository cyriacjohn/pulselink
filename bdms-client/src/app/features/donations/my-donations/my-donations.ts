import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { DonationService } from '../../../core/services/donation.service';
import { ToastrService } from 'ngx-toastr'; 

@Component({
  selector: 'app-my-donations',
  imports: [CommonModule],
  templateUrl: './my-donations.html',
  styleUrl: './my-donations.css',
})
export class MyDonations {
  donations: any[] = [];
  constructor(private http: HttpClient, private donationService: DonationService, private toastr: ToastrService) { }

  ngOnInit() {
    //this.http.get(`${environment.apiUrl}/donation/my`).subscribe((res: any) => {
    //  this.donations = res;
    //});
    this.loadMyDonations();
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

  loadMyDonations() {
    this.http.get(`${environment.apiUrl}/donation/my`).subscribe((res: any) => {
      this.donations = res;
    });
  }

  approve(donorId: number) {
    this.donationService.approve(donorId).subscribe({
      next: () => {
        this.toastr.success('Donation approved');
        this.loadMyDonations();
      }
    });
  }

  reject(donorId: number) {
    this.donationService.reject(donorId).subscribe({
      next: () => {
        this.toastr.warning('Donation rejected');
        this.loadMyDonations();
      }
    });
  }

}

