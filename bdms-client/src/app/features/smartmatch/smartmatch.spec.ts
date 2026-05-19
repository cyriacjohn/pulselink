import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Smartmatch } from './smartmatch';

describe('Smartmatch', () => {
  let component: Smartmatch;
  let fixture: ComponentFixture<Smartmatch>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Smartmatch]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Smartmatch);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
