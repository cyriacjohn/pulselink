import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-thank-you',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './thank-you.html',
  styleUrls: ['./thank-you.css']
})
export class ThankYou implements OnInit {
  certificate: string = '';

  ngOnInit(): void {
    this.certificate = history.state.certificate;
  }
}
