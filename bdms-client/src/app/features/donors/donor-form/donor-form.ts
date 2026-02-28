import {Component, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule, FormBuilder, Validators, FormGroup} from '@angular/forms';
import { Router, ActivatedRoute} from '@angular/router';
import { DonorService } from '../../../core/services/donor.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-donor-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './donor-form.html',
  styleUrl: './donor-form.css',
})
export class DonorForm {
  data;
  donorId: string | null = null;

  constructor(private router: Router, private fb: FormBuilder, private donorService: DonorService, private route: ActivatedRoute, private toastr: ToastrService)
  {
    this.data = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: [null, [Validators.required, Validators.minLength(10), Validators.maxLength(20)]],
      bloodGroup: ['', Validators.required],
      age: [null, [Validators.required, Validators.min(18), Validators.max(65)]],
      address: [null, Validators.required]
    });
    this.donorId = this.route.snapshot.paramMap.get('id');
    if (this.donorId) {
      this.loadDonor(this.donorId);
    }
  }

  loadDonor(id: string) {
    this.donorService.getById(id).subscribe(
      {
        next: (res) => {
          this.data.patchValue(res);
        },
        error: err => console.error(err)
      }
    );
  }

  submit() {
    if (!this.data || this.data.invalid) {
      return;
    }
    if (this.donorId) {
      this.donorService.update(this.donorId, this.data.value).subscribe({
        next: () => {
          this.toastr.success('Donor updated successfully');
          this.router.navigate(['/donors']);
        },
        error: (err: any) => this.toastr.error('Update failed.')
      });
    }
    else {
      this.donorService.create(this.data.value)
        .subscribe({
          next: () => {
            this.toastr.success('Donor created successfully');
            this.router.navigate(['/donors']);
          },
          error: (err: any) => this.toastr.error('Creation failed.')
        });
    }
  }
}
