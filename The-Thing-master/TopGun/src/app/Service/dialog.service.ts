import { Injectable, ComponentRef, ComponentFactoryResolver, ApplicationRef, Injector, EmbeddedViewRef, Type } from '@angular/core';
import { DialogComponent } from '../dialog/dialog.component';
import { DialogConfig } from '../dialog/dialog-config';
import { DialogInjector } from '../dialog/dialog-injector';
import { DialogRef } from '../dialog/dialog-ref';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  dialogComponentRef: ComponentRef<DialogComponent>;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private applicationRef: ApplicationRef,
    private injector: Injector,
    private router: Router
  ) { }

  public open(componentType: Type<any>, dialogConfig: DialogConfig) {
    const dialogRef = this.appendDialogComponentToBody(dialogConfig);

    this.dialogComponentRef.instance.childComponentType = componentType;

    return dialogRef;
  }

  private appendDialogComponentToBody(config: DialogConfig) {
    // create a map with the config
    const map = new WeakMap();
    map.set(DialogConfig, config);

    // add the DialogRef to dependency injection
    const dialogRef = new DialogRef();
    map.set(DialogRef, dialogRef);

    // we want to know when somebody called the close mehtod
    const sub = dialogRef.afterClosed.subscribe(() => {
      // close the dialog
      this.removeDialogComponentFromBody();
      sub.unsubscribe();
    });

    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(DialogComponent);

    // use our new injector
    const componentRef = componentFactory.create(new DialogInjector(this.injector, map));
    this.applicationRef.attachView(componentRef.hostView);

    const domElem = (componentRef.hostView as EmbeddedViewRef<any>).rootNodes[0] as HTMLElement;
    document.body.appendChild(domElem);

    this.dialogComponentRef = componentRef;

    this.closeDialogIfRoutChanged();

    this.dialogComponentRef.instance.onClose.subscribe(() => {
      this.removeDialogComponentFromBody();
    });

  
    return dialogRef;
  }


  private closeDialogIfRoutChanged<T>(): void {
    this.router.events.subscribe(() => {
      this.removeDialogComponentFromBody();
    });
  }

  private removeDialogComponentFromBody() {
    this.applicationRef.detachView(this.dialogComponentRef.hostView);
    this.dialogComponentRef.destroy();
  }




}
