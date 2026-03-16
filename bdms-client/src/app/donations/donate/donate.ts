import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { HospitalService } from '../../core/services/hospital.service';
import { DonationService } from '../../core/services/donation.service';
import { AuthService } from "../../core/services/auth.service"; 

@Component({
  selector: 'app-donate',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './donate.html',
  styleUrl: './donate.css',
})
export class Donate implements OnInit {
  hospitals: any[] = [];
  donation: any = {};
  constructor(private http: HttpClient, private hospitalService: HospitalService, private donationService: DonationService, private authService: AuthService) { }

  ngOnInit() {
    this.hospitalService.getHospitals().subscribe((res: any) => { this.hospitals = res; })
  }

  donate() {
/*    const donorId = this.authService.getUserId()!;*/
    //if (!donorId) {
    //  alert("User not authenticated");
    //  return;
    //}
    const hospitalId = this.donation.hospitalId;
    this.donationService.donateUser(hospitalId).subscribe(() => {
      alert("Donation submitted for approval");
    });
  }
}
