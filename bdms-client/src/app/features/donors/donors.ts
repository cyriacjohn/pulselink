import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DonorService } from '../../core/services/donor.service';
import { Router } from '@angular/router';
import {RouterModule} from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-donors',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './donors.html',
  styleUrl: './donors.css',
})
export class Donors implements OnInit {
  donors: any[] = [];
  pageNumber = 1;
  pageSize = 5;
  totalCount = 0;
  isAdmin = false;
  constructor(private donorService: DonorService, private router: Router, private cdr: ChangeDetectorRef, private authService: AuthService, private toastr: ToastrService) { }
  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
    this.loadDonors();
  }
  loadDonors() { 
    this.donorService.getAll(this.pageNumber, this.pageSize).subscribe(
      {
        next: (res: any) => {
          this.donors = res.data ?? res;
          this.totalCount = res.totalCount ?? 0;
          this.cdr.detectChanges();
        },
        error: (err: any) => {
          console.error(err);
          this.cdr.detectChanges();
        }
      }
    );
  }

  deleteDonor(id: string) {
    if (!confirm('Are you sure you want to delete this donor?')) {
      return;
    }
    this.donorService.delete(id).subscribe({
      next: () => {
        this.toastr.success('Donor deleted');
        this.loadDonors();
      },
      error: err => {
        this.toastr.error('Delete failed');
      }
    });
  }

  nextPage() {
    this.pageNumber++;
    this.loadDonors();
  }

  previousPage() {
    this.pageNumber--;
    this.loadDonors();
  }
}
