import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  model: any = {};
  constructor(private authService: AuthService, private router: Router) { }

  register() {
    this.authService.register(this.model).subscribe({
      next: () => {
        alert: ('Registration successful');
        this.router.navigate(['/login']);
      }
    }
    );
  }
}

