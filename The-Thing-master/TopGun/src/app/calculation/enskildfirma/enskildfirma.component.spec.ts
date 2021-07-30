import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnskildfirmaComponent } from './enskildfirma.component';

describe('EnskildfirmaComponent', () => {
  let component: EnskildfirmaComponent;
  let fixture: ComponentFixture<EnskildfirmaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnskildfirmaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnskildfirmaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
