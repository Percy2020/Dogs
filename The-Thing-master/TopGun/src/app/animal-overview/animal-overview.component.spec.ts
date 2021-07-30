import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnimalOverviewComponent } from './animal-overview.component';

describe('AnimalOverviewComponent', () => {
  let component: AnimalOverviewComponent;
  let fixture: ComponentFixture<AnimalOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnimalOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnimalOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
