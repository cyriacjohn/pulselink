import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = '';
  password = '';
  constructor(private authService: AuthService, private router: Router) { }
  login() {
    const data = {
      username: this.username,
      password: this.password
    }
    this.authService.login(data)
      .subscribe({
        next: (res:any) => {
          this.authService.saveToken(res.token);
          localStorage.setItem("donorId", res.donorId);
          setTimeout(() => {
            if (this.authService.isAdmin()) {
              this.router.navigate(['/dashboard']);
            }
            else {
              this.router.navigate(['/user-dashboard']);
            }
          });
        },
        error: (err:any) => {
          alert('Login failed');
        }
      });
  }
}
