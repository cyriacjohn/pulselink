import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { DonationService } from '../../core/services/donation.service';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../../core/services/notification.service'; 

@Component({
  selector: 'app-donations',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './donations.html',
  styleUrl: './donations.css',
})
export class Donations {
  donations: any[] = [];
  isAdmin = false;
  selectedStatus: number | null = null;

  constructor(private donationService: DonationService, private router: Router, private cdr: ChangeDetectorRef, private authService: AuthService, private notificationService: NotificationService) { }

  ngOnInit() {
    this.isAdmin = this.authService.isAdmin();
    this.loadDonations();
    this.notificationService.donationUpdated.subscribe(() => {
      this.loadDonations();
    })
  }

  loadDonations() {
    this.donationService.getAll(this.selectedStatus).subscribe({
      next: (res: any) => {
        this.donations = res;
      }
    });
  }

  approve(donorId: number) {
    this.donationService.approve(donorId).subscribe({
      next: () => {
        this.loadDonations();
      }
    })
  };

  reject(donorId: number) {
    this.donationService.reject(donorId).subscribe({
      next: () => {
        this.loadDonations();
      }
    }
    )
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

  onStatusChange(event: any) {
    this.selectedStatus = event.target.value === '' ? null : Number(event.target.value);
    this.loadDonations();
  }
}
