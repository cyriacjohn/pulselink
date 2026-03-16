import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PLATFORM_ID, inject, OnInit } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { SpinnerComponent } from '../../../app/shared/components/spinner/spinner';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, RouterModule, SpinnerComponent, CommonModule],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {
  private router = inject(Router);
  private platformId = inject(PLATFORM_ID);
  isAdmin = false;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.isAdmin = this.authService.isAdmin();
  }
  

  logOut() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('token');
      this.router.navigate(['/login']);
    }
  }
}
