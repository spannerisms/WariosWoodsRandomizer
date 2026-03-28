; RNG Table reads
org $83A3AA : JSL RandomL
org $83A3E9 : JSL RandomL
org $83A9E2 : JSL RandomL
org $83B24E : JSR Random
org $83B275 : JSR Random

; IncrementalRandom calls
org $808FD8 : JSL RandomL
org $82A746 : JSL RandomL
org $82A7CF : JSL RandomL
org $83A427 : JSL RandomL
org $83A45A : JSL RandomL
org $83A4D4 : JSL RandomL
org $83A4F9 : JSL RandomL
org $83B7F5 : JSL RandomL
org $83B8F6 : JSL RandomL


org $83DA00

RandomL:
	LDA.l $2137 ; SLVH
	LDA.l $213C ; OPHCT
	ADC.b $DF
	ADC.l SeedSRAM
	STA.l SeedSRAM

	RTL

Random:
	LDA.l $2137 ; SLVH

	LDA.l $213C ; OPHCT
	ADC.b $DF
	ADC.l SeedSRAM
	STA.l SeedSRAM

	RTS


;===================================================================================================

