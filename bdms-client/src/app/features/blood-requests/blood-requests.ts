import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup, FormsModule } from '@angular/forms';
/*import { HttpClient } from '@microsoft/signalr';*/
import { HospitalService } from '../../core/services/hospital.service';
import { getPriority } from 'node:os';
import { error } from 'node:console';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-blood-requests',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './blood-requests.html',
  styleUrl: './blood-requests.css',
})
export class BloodRequests {
  bloodGroup: any[] = [];
  data: any = [];

  constructor(private hospitalService: HospitalService, private fb: FormBuilder, private toastr: ToastrService, private router: Router) {
    this.data = this.fb.group({
      bloodGroup: [null, Validators.required],
      unitsRequired: ['', Validators.required],
      priority: [null, Validators.required]
    })
  }

  ngOnInit() {
    this.hospitalService.getBloodGroups().subscribe((res: any) => {
      this.bloodGroup = res;
    })
  }

  request() {
    if (!this.data || this.data.invalid) {
      return;
    }
    this.hospitalService.createRequest(this.data.value).subscribe({
      next: () => {
        this.toastr.success('Request created successfully');
        this.router.navigate(['/smartmatch']);
      },
      error: (err: any) => this.toastr.error('Request failed.')
    });
  }

}
