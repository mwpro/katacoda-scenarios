The Kubernetes environment is a single node Kubernetes cluster with the Master initialise using Kubeadm.

With the Kubernetes environment configured, users can focus on learning new skills on top of the platform without having to complete the steps to configure the cluster.

To provide users with feedback about when Kubernetes has been fully initialised, run the command `launch.sh`{{execute}}. This indicates to users that something is happening and when it's safe to proceed.

# k8s-lab

Go to https://www.katacoda.com/courses/kubernetes/playground

## Prepare env

```git clone https://github.com/mwpro/k8s-lab.git && cd k8s-lab```{{execute}}

```kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/mandatory.yaml -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/provider/baremetal/service-nodeport.yaml```

## Play around a little

```kubectl```

```kubectl cluster-info```

```kubectl get nodes```

```kubectl describe node node01```

```kubectl get pods```

```kubectl get namespaces```

```kubectl get pods -n kube-system```

```kubectl get pods --all-namespaces```

## Create first pod

https://github.com/mwpro/k8s-lab/tree/master/Lab.Api

https://github.com/mwpro/k8s-lab/blob/master/01.pod.yaml

```kubectl apply -f 01.pod.yaml```

```kubectl get pods```

```kubectl describe pod lab-api```

```ip=$(kubectl describe pod lab-api | grep IP | awk '{print $2}')```

```curl $ip```

## Assign vars to pod

https://github.com/mwpro/k8s-lab/blob/master/02.pod-env.yaml#L11-L13

```kubectl apply -f 02.pod-env.yaml```

```kubectl delete pod lab-api```

```kubectl apply -f 02.pod-env.yaml```

```ip=$(kubectl describe pod lab-api | grep IP | awk '{print $2}')```

```curl $ip```

## First deployment

https://github.com/mwpro/k8s-lab/blob/master/03.deployment.yaml

```kubectl delete -f 02.pod-env.yaml```

```kubectl apply -f 03.deployment.yaml```

```kubectl get deployment```

```kubectl describe deployment lab-api```

```kubectl get pods```

```kubectl describe pod lab-api | grep IP```

```curl xxx```

```kubectl scale deployment lab-api --replicas=10 && watch -n 1 kubectl get pods```

```kubectl edit deployment lab-api && watch -n 1 kubectl get pods```

```kubectl scale deployment lab-api --replicas=3 && watch -n 1 kubectl get pods```

## Service

https://github.com/mwpro/k8s-lab/blob/master/04.service.yaml

```kubectl apply -f 04.service.yaml```

```kubectl get svc```

```kubectl describe service lab-api```

```ip=$(kubectl describe service lab-api | grep IP: | awk '{print $2}')```

```watch -n 1 curl $ip```

## Ingress

https://github.com/mwpro/k8s-lab/blob/master/05.ingress.yaml

```kubectl apply -f 05.ingress.yaml```

```kubectl get ingress```

```kubectl describe ingress lab-ingress```

```ingressIp=$(kubectl describe service ingress-nginx -n ingress-nginx | grep IP: | awk '{print $2}')```

```curl $ip```

```curl --resolve lab.foo:80:$ingressIp lab.foo```

kubectl describe service ingress-nginx -n ingress-nginx

## Frontend deployment

https://github.com/mwpro/k8s-lab/blob/master/06.deployment-2.yaml

```kubectl apply -f 06.deployment-2.yaml```

```kubectl get svc```

```ip=$(kubectl describe service lab-frontend | grep IP: | awk '{print $2}')```

```curl $ip```

## Advanced ingress rules

https://github.com/mwpro/k8s-lab/blob/master/07.ingress-routing.yaml#L12-L19

```kubectl apply -f 07.ingress-routing.yaml```

```curl --resolve lab.foo:80:$ingressIp lab.foo/api```