import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HospitalService } from '../../core/services/hospital.service'; 

@Component({
  selector: 'app-hospital-dashboard',
  imports: [CommonModule],
  templateUrl: './hospital-dashboard.html',
  styleUrl: './hospital-dashboard.css',
})

export class HospitalDashboard {
  constructor(private hospitalService: HospitalService) { }
  dashboard?: HospitalDashboard;
  hospitalName: string = '';
  inventoryCount: number | null = null;
  activeRequests: number | null = null;
  completedDonations: number | null = null;

  ngOnInit() {
    this.hospitalService.getHospitalStats().subscribe(res => {
      this.dashboard = res;
      console.log(res);
    });
  }

}
