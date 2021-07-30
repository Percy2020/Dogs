import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AktiebolagComponent } from './aktiebolag.component';

describe('AktiebolagComponent', () => {
  let component: AktiebolagComponent;
  let fixture: ComponentFixture<AktiebolagComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AktiebolagComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AktiebolagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
